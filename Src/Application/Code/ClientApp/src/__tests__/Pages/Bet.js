import ReactDOM from 'react-dom';
import { act } from 'react-dom/test-utils';
import { MemoryRouter } from 'react-router';
import {Bet} from '../../Pages/Bet';

let container;

beforeEach(() => {
  container = document.createElement('div');
  document.body.appendChild(container);
});

afterEach(() => {
  document.body.removeChild(container);
  container = null;
});

describe(`The bet page`, () => {
  it('can render', () => {
    let pageData = {
      setBreadCrumbs: () => { return true; },
      breadCrumbs: Array([]),
      globalMessage: (alertMessage) => { return; }
    }
    act(() => {
      ReactDOM.render(
        <MemoryRouter>
          <Bet {...pageData} />
        </MemoryRouter>, container);
    });
  });
});

