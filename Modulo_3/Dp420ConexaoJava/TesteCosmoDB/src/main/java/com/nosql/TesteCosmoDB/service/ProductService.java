package com.nosql.TesteCosmoDB.service;


import java.util.UUID;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.azure.cosmos.models.PartitionKey;
import com.nosql.TesteCosmoDB.DTO.ProductDTO;
import com.nosql.TesteCosmoDB.model.Product;
import com.nosql.TesteCosmoDB.repository.ProductRepository;

@Service
public class ProductService {
    @Autowired
    private org.modelmapper.ModelMapper mapper;
    private ProductRepository _productRepository;

    public ProductService(ProductRepository productRepository) {
        this._productRepository = productRepository;
    }
    public Product BuscarProduto(UUID id, String key) {
               
        return _productRepository.findById(id, key);
    }

    public Product SalvarProduto(ProductDTO productDTO) {
        Product product = new Product();
        mapper.map(productDTO, product);
        product.setId(UUID.randomUUID());
        return _productRepository.save(product);
    }
}
