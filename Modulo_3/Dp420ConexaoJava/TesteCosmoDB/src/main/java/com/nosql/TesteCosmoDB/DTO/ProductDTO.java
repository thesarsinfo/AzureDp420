package com.nosql.TesteCosmoDB.DTO;

import java.math.BigDecimal;
import java.util.UUID;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@NoArgsConstructor
@AllArgsConstructor
public class ProductDTO {
    
    private String categoryId;
    private String name;
    private BigDecimal price;
    private String[]  tags; 
}
