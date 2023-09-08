package com.nosql.TesteCosmoDB.controller;

import java.util.UUID;

import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RestController;

import com.nosql.TesteCosmoDB.DTO.ProductDTO;
import com.nosql.TesteCosmoDB.model.Product;
import com.nosql.TesteCosmoDB.service.ProductService;


@RestController
public class ProductController {

    private ProductService _produtoService;

    // @GetMapping
    // public ResponseEntity<Iterable<Product>> buscarTodosPordutos() {
    //     Iterable<Product> listarProdutos = produtoService.buscarTodosProdutos();
    //     return ResponseEntity.ok(listarProdutos);
    // }

    public ProductController(ProductService produtoService) {
        this._produtoService = _produtoService;
    }

     @GetMapping(value = "/buscar/{id}/{categoryId}")
    public ResponseEntity<Product> getProduct(@PathVariable UUID id, @PathVariable String categoryId) {
        Product product = _produtoService.BuscarProduto(id, categoryId);
        return ResponseEntity.ok(product);
    }
    @PostMapping(value = "/cadastro")
    public ResponseEntity<Product> salvarProduto (@RequestBody ProductDTO product) {

        return ResponseEntity.status(201).body(_produtoService.SalvarProduto(product));
    }
}
