import ReactDOM from 'react-dom';
import { act } from 'react-dom/test-utils';
import {Projects} from '../../Pages/Projects';

let container: HTMLDivElement | null;

beforeEach(() => {
  container = document.createElement('div');
  document.body.appendChild(container);
});

afterEach(() =>
{
  if (container !== null)
  {
    document.body.removeChild(container);
    container = null; 
  }
});

describe(`The projects component`, () => {
  it('can render', () => {
    let pageData = {
      setBreadCrumbs: () => { return true; },
      breadCrumbs: Array(),
      globalMessage: () => { return; },
      globalMessageHide: () => { return; }
    }
    act(() => { ReactDOM.render(<Projects {...pageData} />, container); });
  });

  it('can display the add new project form', async () => {
    // Render the projects page
    let pageData = {
      setBreadCrumbs: () => { return true; },
      breadCrumbs: Array(),
      globalMessage: () => { return; },
      globalMessageHide: () => { return; }
    }
    act(() => { ReactDOM.render(<Projects {...pageData} />, container); });

    // The dialog window should not be visible.
    expect(document.getElementById('newModal')).not.toHaveClass("show");

    // Open the new dialog.
    let button = document.getElementById('add-new') as HTMLButtonElement;
    expect(button.innerHTML).not.toBeNull();
    await act(async () => {
      button.dispatchEvent(new MouseEvent("click", { bubbles: true }));
    });
  });
});

