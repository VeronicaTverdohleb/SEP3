﻿using Application.Logic;
using Application.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Model;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderLogic orderLogic;

    public OrderController(IOrderLogic orderLogic)
    {
        this.orderLogic = orderLogic;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Order>>> GetAsync()
    {
        try
        {

            var posts = await orderLogic.GetAllOrdersAsync();
            return Ok(posts);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet, Route("/orders/{id:int}")]
    public async Task<ActionResult<Order>> GetOrderByIdAsync([FromRoute] int id)
    {
        try
        {
            Order order = await orderLogic.GetOrderByIdAsync(id);
            return Ok(order);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
}