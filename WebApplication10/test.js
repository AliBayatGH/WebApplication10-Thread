import http from 'k6/http';
import { check, sleep } from 'k6';

export const options = {
    vus: 100, // Increase number of virtual users
    duration: '30s', // Extend test duration for sustained load
};

export default function () {
    const url = __ENV.TEST_URL || 'http://localhost:5000/api/test/thread';

    const res = http.get(url);

    // Validate response
    check(res, {
        'status is 200': (r) => r.status === 200,
        'response body contains completed': (r) => r.body.includes('completed'),
    });

    // Introduce a variable delay between requests
    sleep(Math.random() * 2); // Random delay between 0-2 seconds
}
