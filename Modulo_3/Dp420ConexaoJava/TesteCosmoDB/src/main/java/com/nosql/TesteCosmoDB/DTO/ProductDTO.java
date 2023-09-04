package com.nosql.TesteCosmoDB.DTO;

import java.math.BigDecimal;
import java.util.UUID;

public class ProductDTO {
    
    private UUID categoryId;
    private String name;
    private BigDecimal price;
    private String[]  tags; 
}
