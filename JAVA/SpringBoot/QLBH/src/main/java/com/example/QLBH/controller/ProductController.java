package com.example.QLBH.controller;

import com.example.QLBH.model.Category;
import com.example.QLBH.model.ImagesProduct;
import com.example.QLBH.model.Product;
import com.example.QLBH.model.Supplier;
import com.example.QLBH.service.ImagesProductService;
import com.example.QLBH.service.ProductService;
import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.core.io.ClassPathResource;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.util.StringUtils;
import org.springframework.validation.BindingResult;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.multipart.MultipartFile;

import java.util.ArrayList;
import java.util.List;


import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.List;
import java.io.File;

import java.io.File;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.UUID;

@Controller
@RequiredArgsConstructor
public class ProductController {
    @Autowired
    private final ProductService productService;
    private final ImagesProductService imagesProductService;

    @GetMapping("/products/add")
    public String showAddForm(Model model) {
        model.addAttribute("product", new Product());
        List<Category> categories = productService.getAllCategories();
        List<Supplier> suppliers = productService.getAllSuppliers();
        model.addAttribute("categories", categories);
        model.addAttribute("suppliers", suppliers);
        return "products/add-product";
    }
    @PostMapping("/products/add")
    public String addProduct(@Valid @ModelAttribute Product product, BindingResult bindingResult, @RequestParam("image") MultipartFile imageFile
                            ,@RequestParam("productimages") MultipartFile[] imageList) {
        if (bindingResult.hasErrors()) {
            return "products/add-product";
        }
        if (!imageFile.isEmpty()) {
            try {
                String imageName = saveImageStatic(imageFile);
                product.setThumbnail("/images/" +imageName);
            } catch (IOException e) {
                e.printStackTrace();
            }
        }


        for (MultipartFile image : imageList) {
            if (!image.isEmpty()) {
                try {
                    String imageUrl = saveImageStatic(image);
                    ImagesProduct productImage = new ImagesProduct();
                    productImage.setPathImage("/images/" +imageUrl);
                    productImage.setProduct(product);
                    product.getImages().add(productImage);
                    imagesProductService.addProductImage(productImage);
                } catch (IOException e) {
                    e.printStackTrace();
                }
            }
        }
        productService.create(product);
        return "redirect:/products";
    }
    @GetMapping("/products")
    public String listProducts(Model model) {
        List<Product> products = productService.findAll();
        model.addAttribute("products", products);
        return "products/products-list";
    }
    @GetMapping("/products/delete/{id}")
    public String deleteProduct(@PathVariable("id") Long id, Model model) {
        Product product = productService.findById(id);
        productService.delete(id);
        model.addAttribute("products", productService.findAll());
        return "redirect:/products";
    }
    @GetMapping("/products/edit/{id}")
    public String showFormDetail(@PathVariable("id") Long id, Model model) {
        Product product = productService.findById(id);
        model.addAttribute("product", product);
        List<Category> categories = productService.getAllCategories();
        List<Supplier> suppliers = productService.getAllSuppliers();
        model.addAttribute("categories", categories);
        model.addAttribute("suppliers", suppliers);
        return "products/update-product";
    }
    @GetMapping("/products/detail/{id}")
    public String showUpdateForm(@PathVariable("id") Long id, Model model) {
        Product product = productService.findById(id);
        model.addAttribute("product", product);
        List<Category> categories = productService.getAllCategories();
        List<Supplier> suppliers = productService.getAllSuppliers();
        model.addAttribute("categories", categories);
        model.addAttribute("suppliers", suppliers);
        return "products/detail-product";
    }
    @PostMapping("/products/update/{id}")
    public String updateCategory(@PathVariable("id") Long id, @Valid Product product,
                                 BindingResult result, Model model,@RequestParam("image") MultipartFile imageFile) {
        if (result.hasErrors()) {
            product.setId(id);
            return "/products/update-product";
        }
        if (!imageFile.isEmpty()) {
            try {
                String imageName = saveImageStatic(imageFile);
                product.setThumbnail("/images/" +imageName);
            } catch (IOException e) {
                e.printStackTrace();
            }
        }
        productService.update(product);
        model.addAttribute("products", productService.findAll());
        return "redirect:/products";
    }


    private String saveImageStatic(MultipartFile image) throws IOException {
        File saveFile = new ClassPathResource("/static/images").getFile();
        String fileName = UUID.randomUUID()+ "." + StringUtils.getFilenameExtension(image.getOriginalFilename());
        Path path = Paths.get(saveFile.getAbsolutePath() + File.separator + fileName);
        Files.copy(image.getInputStream(), path);
        return fileName;
    }
}
