# Expose through API Gatewat following Microservices

Catalog Service:

        AddProduct: [POST] http://localhost:5001/api/products
        EditProduct: [PUT] http://localhost:5001/api/products/{id}
        GetProduct: [GET] http://localhost:5001/api/products/{id}
        GetProducts: [GET] http://localhost:5001/api/products

Promotions Service:

        AddPromotion: [POST] http://localhost:5002/api/promotions
        DeletePromotion: [DELETE] http://localhost:5002/api/promotions/{id}
        GetCurrentPromotion: [GET] http://localhost:5002/api/promotions/{productId}/current
        GetPromotions: [GET] http://localhost:5002/api/promotions

Review Service:

        AddReview: [POST] http://localhost:5003/api/reviews/{productId}
        GetProductReviews: [GET] http://localhost:5003/api/reviews/{productId}
        GetProductReviewsSummary: [GET] http://localhost:5003/api/reviews/{productId}/summary