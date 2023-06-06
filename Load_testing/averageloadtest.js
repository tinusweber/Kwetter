import http from 'k6/http';

export const options = {
  // Key configurations for avg load test in this section
  stages: [
    { duration: '100s', target: 50 }, // traffic ramp-up from 1 to 50 users over 5 minutes.
    { duration: '3m', target: 50 }, // stay at 50 users for 10 minutes
    { duration: '100s', target: 0 }, // ramp-down to 0 users
  ],
};

export default function () {
  http.get('http://192.168.240.3:8081/api/authentication/users');
}