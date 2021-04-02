import ReactDOM from 'react-dom';
import { act } from 'react-dom/test-utils';
import { MemoryRouter } from 'react-router';
import { BreadCrumbItem } from '../../Components/BreadCrumbs';
import { IProject } from '../../Interfaces/IPage';
import { Project } from '../../Pages/Project';
import {ProjectService} from '../../Services/ProjectService';

let container: HTMLElement | null;

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
  it('can render', async () =>
  {
    const project : IProject = { name: "Name", problems: [], isLoaded: false, description: "Description" };
    jest.spyOn(ProjectService, "Get").mockReturnValue(Promise.resolve(project));

    let pageData = {
      setBreadCrumbs: () => { return true; },
      breadCrumbs: Array<BreadCrumbItem>(),
      globalMessage: () => { return; }
    }
    await act( async() => {
      ReactDOM.render(
        <MemoryRouter>
          <Project {...pageData} />
        </MemoryRouter>, container);
    });
  });

  it('can display the add new problem form', async () =>
  {
    const project : IProject = { name: "Name", problems: [], isLoaded: false, description: "Description" };
    jest.spyOn(ProjectService, "Get").mockReturnValue(Promise.resolve(project));

    // Render the projects page
    let pageData = {
      setBreadCrumbs: () => { return true; },
      breadCrumbs: Array<BreadCrumbItem>(),
      globalMessage: () => { return; }
    }
    act(() => {
      ReactDOM.render(
        <MemoryRouter>
          <Project {...pageData} />
        </MemoryRouter>, container);
    });

    // The dialog window should not be visible.
    expect(document.getElementById('newModal')).not.toHaveClass("show");

    // Open the new dialog.
    let button = document.getElementById('add-new');
    expect(button).not.toBeNull();

    if (button !== null)
    {
      expect(button.innerHTML).not.toBeNull();
      await act(async () =>
      {
        if (button !== null)
        {
          button.dispatchEvent(new MouseEvent("click", { bubbles: true }));
        }
      }); 
    }
  });
});

