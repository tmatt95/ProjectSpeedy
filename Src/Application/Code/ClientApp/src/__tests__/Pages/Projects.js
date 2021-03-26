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
    let pageData = {
      setBreadCrumbs: () => { return true;},
      breadCrumbs: Array([]),
      globalMessage: (alertMessage) => { return;}
  }
    act(() => { ReactDOM.render(<Projects {...pageData} />, container); });
  })
});

