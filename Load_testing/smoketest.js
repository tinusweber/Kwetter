import http from 'k6/http';

export const options = {
    // Key configurations for avg load test in this section
    stages: [
      { duration: '5m', target: 50 }, // traffic ramp-up from 1 to 100 users over 5 minutes.
      { duration: '30m', target: 50 }, // stay at 100 users for 10 minutes
      { duration: '5m', target: 0 }, // ramp-down to 0 users
    ],
  };
  

export default function () {
  http.get('http://192.168.240.3:8081/api/authentication/users');
}