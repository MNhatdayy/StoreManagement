package com.example.QLBH;

import lombok.AllArgsConstructor;
@AllArgsConstructor
public enum Role {
    ADMIN(1),
    USER(2),
    MASTER(3);
    public final long value;
    public String getName(){
        return name();
    }
}