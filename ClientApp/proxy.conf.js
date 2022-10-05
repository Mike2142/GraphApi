const { env } = require('process');

const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}/api` :
  env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'http://localhost:55686';

const PROXY_CONFIG = [
  {
    context: [
      "/weatherforecast",
      "/Node",
      "/Edge"
   ],
    target: "https://localhost:7277/api",
    // target: target,
    secure: false,
    headers: {
      Connection: 'Keep-Alive'
    }
  }

  
]

module.exports = PROXY_CONFIG;
