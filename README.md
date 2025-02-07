# Number Classification API 🚀

## Overview
This is a RESTful API that classifies numbers based on their mathematical properties and fetches a fun fact.

## Features
✅ Checks if a number is **prime**, **perfect**, or **Armstrong**  
✅ Determines if a number is **odd** or **even**  
✅ Calculates the **sum of digits** (Handles **negative numbers**)  
✅ Fetches a **fun fact** from the Numbers API  
✅ Returns **error response** for non-numeric inputs  

## API Endpoints
### 📌 Classify a Number
**GET `/api/classify-number/get-number-fact?number={num}`**

#### ✅ Response (200 OK)
```json
{
    "number": 371,
    "is_prime": false,
    "is_perfect": false,
    "properties": ["armstrong", "odd"],
    "digit_sum": 11,
    "fun_fact": "371 is an Armstrong number because 3^3 + 7^3 + 1^3 = 371"
}

### ❌ **Error (400 Bad Request)**  
```json
{
    "number": "abc",
    "error": true,
    "message": "Invalid input. Please provide a valid integer."
}


---

## 🚀 Clone Repository  
```sh
git clone https://github.com/UmukoroHub/NumberClassification
cd number-classification-api

https://hng.tech/hire/csharp-developers