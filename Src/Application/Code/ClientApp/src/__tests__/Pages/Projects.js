import ReactDOM from 'react-dom';
import { act } from 'react-dom/test-utils';
import {Projects} from '../../Pages/Projects';

let container;

beforeEach(() => {
  container = document.createElement('div');
  document.body.appendChild(container);
});

afterEach(() => {
  document.body.removeChild(container);
  container = null;
});

describe(`The projects component`, () => {
  it('can render', () => {
    act(() => { ReactDOM.render(<Projects />, container); });
  })
});

