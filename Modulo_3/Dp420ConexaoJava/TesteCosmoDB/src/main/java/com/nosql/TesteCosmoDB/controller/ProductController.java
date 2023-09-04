package com.nosql.TesteCosmoDB.controller;

import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RestController;

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

    @PostMapping(value = "/cadastro")
    public ResponseEntity<Product> salvarProduto (@RequestBody Product product) {

        return ResponseEntity.status(201).body(_produtoService.SalvarProduto(product));
    }
}
