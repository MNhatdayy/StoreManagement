package com.example.QLBH.repository;

import com.example.QLBH.model.ImagesProduct;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import java.util.List;

@Repository
public interface IImagesProductRepository extends JpaRepository<ImagesProduct, Long> {
    List<ImagesProduct> findByProductId(Long productId);
}
