import http from 'k6/http';

export const options = {
    // Key configurations for spike in this section
    stages: [
      { duration: '2m', target: 2000 }, // fast ramp-up to a high point
      // No plateau
      { duration: '1m', target: 0 }, // quick ramp-down to 0 users
    ],
  };

export default function () {
  http.get('http://192.168.240.3:8081/api/authentication/users');
}