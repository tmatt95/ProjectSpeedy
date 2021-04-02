import ReactDOM from 'react-dom';
import { act } from 'react-dom/test-utils';
import { MemoryRouter } from 'react-router';
import {Problem} from '../../Pages/Problem';

let container;

beforeEach(() => {
  container = document.createElement('div');
  document.body.appendChild(container);
});

afterEach(() => {
  document.body.removeChild(container);
  container = null;
});

describe(`The problem component`, () => {
  it('can render', () => {
    let pageData = {
      setBreadCrumbs: () => { return true; },
      breadCrumbs: Array([]),
      globalMessage: (alertMessage) => { return; }
    }
    act(() => {
      ReactDOM.render(
        <MemoryRouter>
          <Problem {...pageData} />
        </MemoryRouter>, container);
    });
  });

  it('can display the add new bet form', async () => {
    // Render the projects page
    let pageData = {
      setBreadCrumbs: () => { return true; },
      breadCrumbs: Array([]),
      globalMessage: (alertMessage) => { return; }
    }
    act(() => {
      ReactDOM.render(
        <MemoryRouter>
          <Problem {...pageData} />
        </MemoryRouter>, container);
    });

    // The dialog window should not be visible.
    expect(document.getElementById('newModal')).not.toHaveClass("show");

    // Open the new dialog.
    let button = document.getElementById('add-new');
    expect(button.innerHTML).not.toBeNull();
    await act(async () => {
      button.dispatchEvent(new MouseEvent("click", { bubbles: true }));
    });
  });
});

