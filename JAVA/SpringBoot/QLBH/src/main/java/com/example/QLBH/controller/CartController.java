package com.example.QLBH.controller;


import com.example.QLBH.model.CartItem;
import com.example.QLBH.service.CartService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@Controller
@RequestMapping("/cart")
public class CartController {
    @Autowired
    private CartService cartService;
    @GetMapping
    public String showCart(Model model) {
        List<CartItem> cartItems = cartService.getCartItems();
        double tongTien = cartItems.stream().mapToDouble(item -> item.getQuantity() * item.getProduct().getPrice()).sum();
        Long total = (long) tongTien;
        model.addAttribute("cartItems", cartService.getCartItems());
        model.addAttribute("total", total);
        return "/cart/cart";
    }
    @PostMapping("/add")
    public String addToCart(@RequestParam Long productId, @RequestParam int
            quantity) {
        cartService.addToCart(productId, quantity);
        return "redirect:/products";
    }
    @GetMapping("/remove/{productId}")
    public String removeFromCart(@PathVariable Long productId) {
        cartService.removeFromCart(productId);
        return "redirect:/cart";
    }
    @GetMapping("/clear")
    public String clearCart() {
        cartService.clearCart();
        return "redirect:/cart";
    }
    @GetMapping("/increase/{productId}")
    public String increaseQuantity(@PathVariable Long productId) {
        cartService.increase(productId);
        return "redirect:/cart";
    }

    @GetMapping("/decrease/{productId}")
    public String decreaseQuantity(@PathVariable Long productId) {
        cartService.decrease(productId);
        return "redirect:/cart";
    }
}
