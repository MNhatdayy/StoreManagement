package com.example.QLBH.controller;

import com.example.QLBH.DTO.PaymentDTO;
import com.example.QLBH.model.CartItem;

import com.example.QLBH.model.Order;
import com.example.QLBH.response.ResponseObject;
import com.example.QLBH.service.CartService;
import com.example.QLBH.service.OrderService;
import com.example.QLBH.service.PaymentService;
import org.springframework.core.ParameterizedTypeReference;
import org.springframework.http.*;
import jakarta.servlet.ServletException;
import jakarta.servlet.http.HttpServletRequest;
import jakarta.servlet.http.HttpServletResponse;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.client.RestTemplate;

import java.io.IOException;
import java.net.URI;
import java.util.List;

@Controller
@RequestMapping("/order")
public class OrderController {
    @Autowired
    private OrderService orderService;
    @Autowired
    private CartService cartService;
    @Autowired
    private final PaymentService paymentService;
    @Autowired
    private final RestTemplate restTemplate;

    public OrderController(RestTemplate restTemplate, CartService cartService, OrderService orderService, PaymentService paymentService) {
        this.restTemplate = restTemplate;
        this.cartService = cartService;
        this.orderService = orderService;
        this.paymentService = paymentService;
    }

    @GetMapping("/checkout")
    public String checkout() {

        return "/cart/checkout";
    }
    @PostMapping("/submit")
    public String submitOrder(@RequestParam String customerName, @RequestParam String payment, HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        List<CartItem> cartItems = cartService.getCartItems();
        if (cartItems.isEmpty()) {
            return "redirect:/cart"; // Redirect if cart is empty
        }
        if ("cod".equals(payment)) {
            orderService.createOrder(customerName, cartItems);
            return "redirect:/order/confirmation";
        } else if ("vnpay".equals(payment)) {
            double total = cartService.getCartItems().stream().mapToDouble(item -> item.getQuantity() * item.getProduct().getPrice()).sum();
            orderService.createOrder(customerName, cartItems);
            Long tongTien = (long) total;
            Long totalDone = tongTien * 100 ;
            paymentService.createVnPayPayment(request,totalDone);
            PaymentDTO.VNPayResponse payResponse = paymentService.createVnPayPayment(request,totalDone);
            String Payurl = payResponse.getPaymentUrl();
            return "redirect:" + Payurl;
        }
        return "redirect:/cart";
    }
    @GetMapping("/confirmation")
    public String orderConfirmation(Model model) {
        model.addAttribute("message", "Your order has been successfully placed.");
        return "/cart/order-confirmation";
    }
}
