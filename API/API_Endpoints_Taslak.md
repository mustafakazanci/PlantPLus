# Plant Disease Detection API Endpoint Taslağı

## DiseaseController
- `GET /api/diseases`: Tüm hastalıkları getirir.
- `GET /api/diseases/{id}`: Belirli bir hastalığı ID'sine göre getirir.
(Not: Hastalık verileri statik olacağı için POST/PUT/DELETE eklenmeyecektir.)

## PredictionController
- `POST /api/prediction`: Görüntü alır, ML modeli ile hastalığı tespit eder ve çözüm önerilerini döner. 