# Congestion Tax Calculator API

## Overview

The Congestion Tax Calculator is a web API designed to calculate congestion tax fees for vehicles in Gothenburg, Sweden. The application uses a robust architecture that employs the Strategy and Factory design patterns at the domain level, ensuring it adheres closely to the Open/Closed Principle (OCP).

Each pass is taxed based on the highest amount of tax of all passes that come within one hour after it.

### Features

- Calculate congestion tax based on vehicle pass times.
- Create, update, retrieve, and delete pass records.
- Extensible architecture for integrating external services to obtain tax rates.
- Dynamic retrieval of rules from the database.
- Pass the following JSON to the Calculate Tax API to get the existing pass taxes:

```json
{
  "vehicleId": "11111111-1111-1111-1111-111111111111",
  "city": 0
}
