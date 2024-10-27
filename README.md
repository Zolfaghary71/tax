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
```

which would result in this
| **Formatted Pass DateTime**    | **Actual Tax (SEK)** |
|---------------------------------|-----------------------|
| 2013-01-14 21:00:00             | 0                     |
| 2013-01-15 21:00:00             | 0                     |
| 2013-02-07 06:23:27             | 8                     |
| 2013-02-07 15:27:00             | 13                    |
| 2013-02-08 06:20:27             | 8                     |
| 2013-02-08 06:27:00             | 0                     |
| 2013-02-08 14:35:00             | 13                    |
| 2013-02-08 15:29:00             | 0                     |
| 2013-02-08 15:47:00             | 18                    |
| 2013-02-08 16:01:00             | 0                     |
| 2013-02-08 16:48:00             | 18                    |
| 2013-02-08 17:49:00             | 3                     |
| 2013-02-08 18:29:00             | 0                     |
| 2013-02-08 18:35:00             | 0                     |
| 2013-03-26 14:25:00             | 8                     |
| 2013-03-28 14:07:27             | 8                     |
