package com.nosql.TesteCosmoDB.repository;

import com.azure.spring.data.cosmos.repository.CosmosRepository;
import com.nosql.TesteCosmoDB.model.Product;

import java.util.Optional;
import java.util.UUID;

import org.springframework.stereotype.Repository;

@Repository
public interface ProductRepository extends CosmosRepository<Product,UUID> {
   
    Product findById(UUID id, String key);    

}
