# 🛒 Ecommerce Microservices (Clean Architecture)

Hands-on learning project for building **E-commerce Microservices** using **ASP.NET Core**, **Clean Architecture**, and **Docker**.

The goal of this project is to practice **real-world backend architecture**, service separation, and scalable microservices design.

---

## 🧱 Architecture Overview

- Microservices-based architecture
- Clean Architecture per service
- Database per service
- Cache-first design where applicable
- Dockerized local development environment

---

## ✅ Implemented Services

### 📦 Catalog Service
- ASP.NET Core Web API
- MongoDB (NoSQL)
- Clean Architecture layers:
  - `Catalog.API`
  - `Catalog.Application`
  - `Catalog.Core`
  - `Catalog.Infrastructure`

### 🛒 Basket Service
- ASP.NET Core Web API
- Redis (In-Memory Cache)
- Clean Architecture layers:
  - `Basket.API`
  - `Basket.Application`
  - `Basket.Core`
  - `Basket.Infrastructure`
- Shopping Cart & Checkout domain models
- Repository abstraction (`IBasketRepository`)

---

## 📁 Project Structure

```text
services/
 ├── catalog/
 │   ├── Catalog.API
 │   ├── Catalog.Application
 │   ├── Catalog.Core
 │   └── Catalog.Infrastructure
 │
 ├── basket/
 │   ├── Basket.API
 │   ├── Basket.Application
 │   ├── Basket.Core
 │   └── Basket.Infrastructure
 │
docker-compose.yml
