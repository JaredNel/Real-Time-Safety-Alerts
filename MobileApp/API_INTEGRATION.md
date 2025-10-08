# Backend API Integration Guide

This document describes the expected API endpoints and data contracts for integrating the Real-Time Safety Alerts mobile application with a backend service.

## Base URL

The API base URL is configured in `Services/SafetyAlertService.cs`:
```
https://api.safetyalerts.com/api/v1
```

## Authentication

For future implementation, consider using:
- JWT tokens for authenticated requests
- API keys for service identification
- OAuth 2.0 for user authentication

## API Endpoints

### 1. Get All Alerts

Retrieve all active safety alerts.

**Endpoint:** `GET /alerts`

**Response:** `200 OK`
```json
[
  {
    "id": 1,
    "title": "Severe Weather Alert",
    "description": "Heavy rain and thunderstorms expected...",
    "severity": 2,
    "type": 0,
    "location": {
      "latitude": 40.7128,
      "longitude": -74.0060,
      "address": "123 Main St",
      "city": "New York",
      "state": "NY",
      "zipCode": "10001"
    },
    "timestamp": "2024-01-15T14:30:00Z",
    "isActive": true,
    "imageUrl": null
  }
]
```

### 2. Get Alert by ID

Retrieve a specific alert by its ID.

**Endpoint:** `GET /alerts/{id}`

**Parameters:**
- `id` (path parameter): Alert ID

**Response:** `200 OK`
```json
{
  "id": 1,
  "title": "Severe Weather Alert",
  "description": "Heavy rain and thunderstorms expected...",
  "severity": 2,
  "type": 0,
  "location": {
    "latitude": 40.7128,
    "longitude": -74.0060,
    "address": "123 Main St",
    "city": "New York",
    "state": "NY",
    "zipCode": "10001"
  },
  "timestamp": "2024-01-15T14:30:00Z",
  "isActive": true,
  "imageUrl": null
}
```

### 3. Get Alerts by Location

Retrieve alerts within a specified radius of a location.

**Endpoint:** `GET /alerts/location`

**Query Parameters:**
- `lat` (required): Latitude
- `lon` (required): Longitude
- `radius` (required): Search radius in kilometers

**Example:** `/alerts/location?lat=40.7128&lon=-74.0060&radius=10`

**Response:** `200 OK`
```json
[
  {
    "id": 1,
    "title": "Severe Weather Alert",
    "description": "Heavy rain and thunderstorms expected...",
    "severity": 2,
    "type": 0,
    "location": {
      "latitude": 40.7128,
      "longitude": -74.0060,
      "address": "123 Main St",
      "city": "New York",
      "state": "NY",
      "zipCode": "10001"
    },
    "timestamp": "2024-01-15T14:30:00Z",
    "isActive": true,
    "imageUrl": null
  }
]
```

## Data Models

### SafetyAlert

| Field | Type | Description |
|-------|------|-------------|
| id | integer | Unique identifier |
| title | string | Alert title |
| description | string | Detailed description |
| severity | integer | Severity level (0=Low, 1=Medium, 2=High, 3=Critical) |
| type | integer | Alert type (0=Weather, 1=Crime, 2=Traffic, 3=Fire, 4=Medical, 5=Environmental, 6=Other) |
| location | Location | Location details |
| timestamp | datetime | When alert was created (ISO 8601) |
| isActive | boolean | Whether alert is currently active |
| imageUrl | string (nullable) | Optional image URL |

### Location

| Field | Type | Description |
|-------|------|-------------|
| latitude | number | Latitude coordinate |
| longitude | number | Longitude coordinate |
| address | string | Street address |
| city | string | City name |
| state | string | State/province |
| zipCode | string | Postal code |

### AlertSeverity Enum

```
0 = Low
1 = Medium
2 = High
3 = Critical
```

### AlertType Enum

```
0 = Weather
1 = Crime
2 = Traffic
3 = Fire
4 = Medical
5 = Environmental
6 = Other
```

## Error Responses

### 400 Bad Request
```json
{
  "error": "Invalid request parameters",
  "message": "Latitude must be between -90 and 90"
}
```

### 404 Not Found
```json
{
  "error": "Alert not found",
  "message": "No alert exists with ID: 123"
}
```

### 500 Internal Server Error
```json
{
  "error": "Internal server error",
  "message": "An unexpected error occurred"
}
```

## Rate Limiting

Recommended rate limits:
- 100 requests per minute per IP
- 1000 requests per hour per user

**Headers:**
- `X-RateLimit-Limit`: Maximum requests allowed
- `X-RateLimit-Remaining`: Requests remaining
- `X-RateLimit-Reset`: Timestamp when limit resets

## CORS Configuration

For web/mobile app integration:
```
Access-Control-Allow-Origin: *
Access-Control-Allow-Methods: GET, POST, PUT, DELETE
Access-Control-Allow-Headers: Content-Type, Authorization
```

## WebSocket Support (Future)

For real-time push notifications:

**Endpoint:** `wss://api.safetyalerts.com/ws`

**Message Format:**
```json
{
  "type": "new_alert",
  "data": {
    "id": 5,
    "title": "Emergency Alert",
    "severity": 3,
    "location": {...}
  }
}
```

## Testing

### Mock Server

For development, use the built-in mock data in `SafetyAlertService.cs` which provides sample alerts when the backend is unavailable.

### Testing Tools

- **Postman**: Test API endpoints
- **curl**: Command-line testing
- **Swagger/OpenAPI**: API documentation and testing UI

### Example curl Commands

```bash
# Get all alerts
curl -X GET https://api.safetyalerts.com/api/v1/alerts

# Get alert by ID
curl -X GET https://api.safetyalerts.com/api/v1/alerts/1

# Get alerts by location
curl -X GET "https://api.safetyalerts.com/api/v1/alerts/location?lat=40.7128&lon=-74.0060&radius=10"
```

## Implementation Checklist

- [ ] Backend API server setup
- [ ] Database schema implementation
- [ ] API endpoints implementation
- [ ] Authentication/authorization
- [ ] Rate limiting
- [ ] Error handling
- [ ] API documentation (Swagger)
- [ ] Unit tests
- [ ] Integration tests
- [ ] Performance testing
- [ ] Security audit
- [ ] Production deployment

## Security Considerations

1. **HTTPS Only**: All API communication must use HTTPS
2. **Input Validation**: Validate all incoming data
3. **SQL Injection Protection**: Use parameterized queries
4. **Rate Limiting**: Prevent abuse
5. **Authentication**: Secure endpoints requiring user context
6. **Data Privacy**: Comply with GDPR/CCPA regulations
7. **API Keys**: Rotate regularly and store securely

## Support

For API integration issues:
1. Check the mock data implementation for expected format
2. Review error responses for debugging information
3. Enable debug logging in the mobile app
4. Contact the backend team with specific error details
