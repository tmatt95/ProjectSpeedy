import ReactDOM from 'react-dom';
import { act } from 'react-dom/test-utils';
import ProjectNewForm from '../../../Components/Projects/ProjectNewForm.tsx';

let container;

beforeEach(() => {
  container = document.createElement('div');
  document.body.appendChild(container);
});

afterEach(() => {
  document.body.removeChild(container);
  container = null;
});

describe(`The project new form component`, () => {
  it('can render', () => {
    act(() => { ReactDOM.render(<ProjectNewForm />, container); });
  })
});

