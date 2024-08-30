package com.example.QLBH.model;
import jakarta.persistence.*;
import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.RequiredArgsConstructor;
import lombok.Setter;

import java.util.ArrayList;
import java.util.List;

@Setter
@Getter
@RequiredArgsConstructor
@AllArgsConstructor
@Entity
@Table(name = "products")
public class Product {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;
    private String name;
    private double price;
    private String description;
    @ManyToOne
    @JoinColumn(name = "category_id")
    private Category category;

    @ManyToOne
    @JoinColumn(name = "supplier_id", nullable = false)
    private Supplier supplier;
    private String thumbnail;

    @OneToMany(mappedBy = "product", cascade = CascadeType.ALL, orphanRemoval = true )
    private List<ImagesProduct> images = new ArrayList<>();

    public List<ImagesProduct> getImages() {
        return images;
    }

    public void setImages(List<ImagesProduct> images) {
        this.images = images;
    }

}
