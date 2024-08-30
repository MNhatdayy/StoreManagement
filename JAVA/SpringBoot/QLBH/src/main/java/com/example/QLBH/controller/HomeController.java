package com.example.QLBH.controller;

import com.example.QLBH.model.Product;
import com.example.QLBH.service.CategoryService;
import com.example.QLBH.service.ImagesProductService;
import com.example.QLBH.service.ProductService;
import lombok.RequiredArgsConstructor;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;

import java.util.List;

@RequestMapping()
@Controller
@RequiredArgsConstructor
public class HomeController {
    @Autowired
    private final ProductService productService;
    private final CategoryService categoryService;
    @GetMapping()
    public String getAllProducts(Model model) {

        model.addAttribute("products", productService.findAll());
        model.addAttribute("category", productService.getAllCategories());
        model.addAttribute("suplliers", productService.getAllSuppliers());
        return "home/home";
    }
}