package com.example.QLBH.repository;

import com.example.QLBH.model.OrderDetail;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;


@Repository
public interface IOrderDetailRepository extends JpaRepository<OrderDetail, Long> {

}
