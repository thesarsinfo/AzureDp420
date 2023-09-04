package com.nosql.TesteCosmoDB.model;

import java.math.BigDecimal;
import java.util.UUID;

import com.azure.spring.data.cosmos.core.mapping.Container;
import com.azure.spring.data.cosmos.core.mapping.PartitionKey;

import nonapi.io.github.classgraph.json.Id;

@Container(containerName = "Product", ru = "400")

public class Product {
    @Id
    private String id;
    @PartitionKey
    private String categoryId;
    private String name;
    private BigDecimal price;
    private String[]  tags;

    public String getId() {
        return id;
    }
    public void setId(String id) {
        this.id = id;
    }
    public String getCategoryId() {
        return categoryId;
    }
    public void setCategoryId(String categoryId) {
        this.categoryId = categoryId;
    }
    public String getName() {
        return name;
    }
    public void setName(String name) {
        this.name = name;
    }
    public BigDecimal getPrice() {
        return price;
    }
    public void setPrice(BigDecimal price) {
        this.price = price;
    }
    public String[] getTags() {
        return tags;
    }
    public void setTags(String[] tags) {
        this.tags = tags;
    } 
   
}
