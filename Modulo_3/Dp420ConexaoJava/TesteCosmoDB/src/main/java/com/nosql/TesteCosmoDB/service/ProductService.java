package com.nosql.TesteCosmoDB.service;


import org.springframework.stereotype.Service;

import com.nosql.TesteCosmoDB.model.Product;
import com.nosql.TesteCosmoDB.repository.ProductRepository;

@Service
public class ProductService {
    
    private ProductRepository _productRepository;

    public ProductService(ProductRepository productRepository) {
        this._productRepository = productRepository;
    }

    public Product SalvarProduto(Product product) {
        return _productRepository.save(product);
    }
}
