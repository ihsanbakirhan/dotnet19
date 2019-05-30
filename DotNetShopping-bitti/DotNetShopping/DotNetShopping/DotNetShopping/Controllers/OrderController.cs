﻿using DotNetShopping.Helpers;
using DotNetShopping.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DotNetShopping.Controllers
{
    public class OrderController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Order
        public ActionResult Index()
        {
            var model = db.Orders.OrderByDescending(x => x.OrderId)
                .OrderByDescending(x => x.OrderId)
                .Select(x => new OrderListModel
                {
                    OrderId = x.OrderId,
                    UserId = x.UserId,
                    OrderDate = x.OrderDate,
                    OrderStatus = x.OrderStatus,
                    Paid = x.Paid,
                    TotalPrice = x.TotalPrice,
                    Email = db.Users.Where(u => u.Id == x.UserId).FirstOrDefault().Email,
                    UserName = x.BillingFirstName + " " + x.BillingLastName
                });
            return View(model.ToList());
        }
        public ActionResult Detail(Int64 id)
        {
            var model = db.Orders.Where(x => x.OrderId == id)
                .Join(db.ShippingMethods, o => o.ShippingMethodId, sm => sm.ShippingMethodId, (o, sm) => new { Order = o, ShippingMethod = sm })
                .Join(db.PaymentMethods, osm => osm.Order.PaymentMethodId, pm => pm.PaymentMethodId, (osm, pm) => new { osm, PaymentMethod = pm })
                .Select(x => new OrderDetailModel
                {
                    OrderId = x.osm.Order.OrderId,
                    OrderDate = x.osm.Order.OrderDate,
                    OrderStatus = x.osm.Order.OrderStatus,
                    Paid = x.osm.Order.Paid,
                    PaymentMethodId = x.osm.Order.PaymentMethodId,
                    PaymentMethodName = x.PaymentMethod.Name,
                    ShippingCityId = x.osm.Order.ShippingCityId,
                    ShippingCompany = x.osm.Order.ShippingCompany,
                    ShippingCost = x.osm.Order.ShippingCost,
                    ShippingCountryId = x.osm.Order.ShippingCountryId,
                    ShippingDate = x.osm.Order.ShippingDate,
                    ShippingCode = x.osm.Order.ShippingCode,
                    ShippingFirstName = x.osm.Order.ShippingFirstName,
                    ShippingLastName = x.osm.Order.ShippingLastName,
                    ShippingMethodId = x.osm.Order.ShippingMethodId,
                    ShippingMethodName = x.osm.ShippingMethod.Name,
                    ShippingStateId = x.osm.Order.ShippingStateId,
                    ShippingStreet1 = x.osm.Order.ShippingStreet1,
                    ShippingStreet2 = x.osm.Order.ShippingStreet2,
                    ShippingTelephone = x.osm.Order.ShippingTelephone,
                    ShippingZip = x.osm.Order.ShippingZip,
                    TotalPrice = x.osm.Order.TotalPrice,
                    UserId = x.osm.Order.UserId,
                    Discount = x.osm.Order.Discount,
                    CityName = db.Cities.Where(c => c.CityId == x.osm.Order.ShippingCityId).FirstOrDefault().Name,
                    StateName = db.States.Where(s => s.StateId == x.osm.Order.ShippingStateId).FirstOrDefault().Name,
                    CountryName = db.Countries.Where(c => c.CountryId == x.osm.Order.ShippingCountryId).FirstOrDefault().Name,
                    OrderProducts = db.OrderProducts.Where(op => op.OrderId == id)
                    .Join(db.Variants, op => op.VariantId, v => v.VariantId, (op, v) => new { OrderProduct = op, Variant = v })
                    .Join(db.Products, opv => opv.Variant.ProductId, p => p.ProductId, (opv, p) => new { opv, Product = p })
                    .Select(oplm => new OrderProductListModel
                    {
                        OrderId = oplm.opv.OrderProduct.OrderId,
                        Cost = oplm.opv.OrderProduct.Cost,
                        Quantity = oplm.opv.OrderProduct.Quantity,
                        TotalCost = oplm.opv.OrderProduct.TotalCost,
                        TotalPrice = oplm.opv.OrderProduct.TotalPrice,
                        UnitPrice = oplm.opv.OrderProduct.UnitPrice,
                        VariantId = oplm.opv.OrderProduct.VariantId,
                        ProductId = oplm.Product.ProductId,
                        ProductName = oplm.Product.Name,
                        VariantName = oplm.opv.Variant.Name,
                        FileName = db.ProductImages.Where(pi => pi.VariantId == oplm.opv.Variant.VariantId).OrderBy(pi => pi.Sequence).FirstOrDefault().FileName
                    }).ToList()
                }).FirstOrDefault();
            return View(model);
        }


        [HttpPost]
        public ActionResult UpdateStatus(Order.OrderStatuses status , string shippingCode , Int64 orderId)
        {
            try
            {
                if (User.IsInRole("Admin") || User.IsInRole("Order Manager"))
                {
                    var order = db.Orders.Find(orderId);
                    if(order != null)
                    {
                        order.OrderStatus = status;
                        if(shippingCode != "")
                        {
                            order.ShippingCode = shippingCode;
                            order.ShippingDate = DateTime.Now;
                            
                        }
                        db.SaveChanges();
                        if(status == Order.OrderStatuses.Shipped)
                        {
                            var emailHelper = new EmailHelper();
                            var userId = User.Identity.GetUserId();
                            var user = db.Users.Find(userId);
                            emailHelper.SendOrderShipped(orderId, Request, Url, user);
                        }
                        return Json(new { Success = true });
                    }
                    else
                    {
                        return Json(new { Success = false, Error = "Order not found." });
                    }
                    
                }
                else
                {
                    return Json(new { Success = false, Error = "Permission Denied" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Error = ex.Message });
                
            }
            
            
        }
    }

}