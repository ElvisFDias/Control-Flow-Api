import http from 'k6/http';
import { Counter } from 'k6/metrics';
import { vu } from 'k6/execution';
import { SharedArray } from 'k6/data';

const myCounter = new Counter('my_counter');
const url1 = 'http://localhost:5283/Usuario/CriarUsuario_ComException'
const url2 = 'http://localhost:5283/Usuario/CriarUsuario_SemExcecao'
const users = new SharedArray('users', function () {
    return JSON.parse(open('./data.json')).users;
  });

export const options = {
    scenarios: {
      'use-all-the-data': {
        executor: 'per-vu-iterations',
        vus: 1,
        iterations: 1000,
        maxDuration: '300s',
      },
    },
  };

export default function () {
    let index = vu.iterationInScenario % 5; 
    let user = users[index];

    let res = http.post(url2, JSON.stringify(user), {
        headers: { 'Content-Type': 'application/json' },
      });
}