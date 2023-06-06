import http from 'k6/http';

export const options = {
    // Key configurations for breakpoint in this section
    executor: 'ramping-arrival-rate', //Assure load increase if the system slows

    stages: [
      { duration: '2h', target: 20000 }, // just slowly ramp-up to a HUGE load
    ],
    thresholds: {
        metric_name: [
          {
            threshold: 'p(99) < 10', // string
            abortOnFail: true, // boolean
            delayAbortEval: '10s', // string
            /*...*/
          },
        ],
      }
  };
  

export default function () {
  http.get('http://192.168.240.3:8081/api/authentication/users');
}