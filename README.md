fetch('https://your-api-url/api/exemption/check-exemptions?vendorCode=17201&workOrders=4700321', {
  headers: {
    'ApiKey': 'your-secret-api-key'
  }
})
.then(response => response.json())
.then(data => console.log(data));
