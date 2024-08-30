package com.example.QLBH;

import org.springframework.context.annotation.Configuration;
import org.springframework.web.servlet.config.annotation.ResourceHandlerRegistry;
import org.springframework.web.servlet.config.annotation.WebMvcConfigurer;

@Configuration
public class MvcConfig implements WebMvcConfigurer {

    @Override
    public void addResourceHandlers(ResourceHandlerRegistry registry) {
        String absolutePath = System.getProperty("user.dir") + "/product-images/";
        registry.addResourceHandler("/product-images/**")
                .addResourceLocations("file:" + absolutePath);
    }

}
