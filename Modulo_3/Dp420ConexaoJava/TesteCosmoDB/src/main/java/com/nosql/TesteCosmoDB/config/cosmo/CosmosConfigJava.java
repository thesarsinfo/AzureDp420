package com.nosql.TesteCosmoDB.config.cosmo;

import java.util.Arrays;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.boot.context.properties.EnableConfigurationProperties;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.lang.Nullable;

import com.azure.core.credential.AzureKeyCredential;
import com.azure.cosmos.CosmosClient;
import com.azure.cosmos.CosmosClientBuilder;
import com.azure.cosmos.DirectConnectionConfig;
import com.azure.cosmos.models.CosmosDatabaseResponse;
import com.azure.cosmos.models.ThroughputProperties;
import com.azure.spring.data.cosmos.config.AbstractCosmosConfiguration;
import com.azure.spring.data.cosmos.config.CosmosConfig;
import com.azure.spring.data.cosmos.core.ResponseDiagnostics;
import com.azure.spring.data.cosmos.core.ResponseDiagnosticsProcessor;
import com.azure.spring.data.cosmos.repository.config.EnableCosmosRepositories;
import com.azure.spring.data.cosmos.repository.config.EnableReactiveCosmosRepositories;

@Configuration
@EnableConfigurationProperties(CosmosProperties.class)
@EnableCosmosRepositories(basePackages = "com.nosql.TesteCosmoDB.repository")
@EnableReactiveCosmosRepositories
public class CosmosConfigJava extends AbstractCosmosConfiguration {

    private static final Logger logger = LoggerFactory.getLogger(CosmosConfig.class);
    private CosmosProperties propertiesCosmos;

    public CosmosConfigJava(CosmosProperties properties){
        this.propertiesCosmos = properties;
    }
    @Bean
    public CosmosClientBuilder cosmosClientBuilder() {
        AzureKeyCredential key = new AzureKeyCredential(propertiesCosmos.getKey());
 

        DirectConnectionConfig directConnectionConfig = DirectConnectionConfig.getDefaultConfig();
        
        return new CosmosClientBuilder()
        .endpoint(propertiesCosmos.getUri())
        .credential(key)        
        .directMode(directConnectionConfig);
        
    }
    @Bean
    public CosmosConfig cosmosConfig() {
        CosmosClient client = new CosmosClientBuilder()        
            .endpoint(propertiesCosmos.getUri())
            .key(propertiesCosmos.getKey())
            .buildClient();
        client.createDatabaseIfNotExists(propertiesCosmos.getDatabase());
        return CosmosConfig.builder()
                .responseDiagnosticsProcessor(new ResponseDiagnosticsProcessorImplementation())
                .enableQueryMetrics(propertiesCosmos.isQueryMetricsEnabled())
                .build();
    }
    @Override
    protected String getDatabaseName() {
        return propertiesCosmos.getDatabase();
    }
        private static class ResponseDiagnosticsProcessorImplementation implements ResponseDiagnosticsProcessor {

        @Override
        public void processResponseDiagnostics(@Nullable ResponseDiagnostics responseDiagnostics) {
            logger.info("Response Diagnostics {}", responseDiagnostics);
        }
    }
}
