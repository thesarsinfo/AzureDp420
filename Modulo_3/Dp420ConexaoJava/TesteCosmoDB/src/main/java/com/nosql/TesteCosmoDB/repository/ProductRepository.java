package com.nosql.TesteCosmoDB.repository;

import com.azure.spring.data.cosmos.repository.CosmosRepository;
import com.nosql.TesteCosmoDB.model.Product;

import org.springframework.stereotype.Repository;

@Repository
public interface ProductRepository extends CosmosRepository<Product,String> {
    

}
