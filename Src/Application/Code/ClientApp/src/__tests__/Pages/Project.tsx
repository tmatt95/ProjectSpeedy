import { fireEvent } from '@testing-library/react';
import ReactDOM from 'react-dom';
import { act } from 'react-dom/test-utils';
import { MemoryRouter } from 'react-router';
import { BreadCrumbItem } from '../../Components/BreadCrumbs';
import { IProject } from '../../Interfaces/IPage';
import { Project } from '../../Pages/Project';
import { ProblemService } from '../../Services/ProblemService';
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

    var myBlob = new Blob();
    var init = { "status" : 202 , "statusText" : "SuperSmashingGreat!" };
    var myResponse = new Response(myBlob,init);
    jest.spyOn(ProjectService, "Put").mockReturnValue(Promise.resolve(myResponse));


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
    let button = document.getElementById('problem-bet-new-create');
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

  it('displays an error next to the name if one sent back from the server on creation of problem', async () =>
  {
    const problem: IProject = { name: "Name", problems: [], isLoaded: false, description: "Description" };
    jest.spyOn(ProjectService, "Get").mockReturnValue(Promise.resolve(problem));

    var myBlob = new Blob();
    var init = { "status" : 202  };
    var myResponse = new Response(myBlob,init);
    jest.spyOn(ProblemService, "Put").mockReturnValue(Promise.resolve(myResponse));

    // Render the projects page
    let pageData = {
      setBreadCrumbs: () => { return true; },
      breadCrumbs: Array<BreadCrumbItem>(),
      globalMessage: () => { return; }
    }
    act(() =>
    {
      ReactDOM.render(
        <MemoryRouter>
          <Project {...pageData} />
        </MemoryRouter>, container);
    });

    // The dialog window should not be visible.
    expect(document.getElementById('newModal')).not.toHaveClass("show");

    // Open the new dialog.
    let button = document.getElementById('add-new') as HTMLButtonElement;
    expect(button).not.toBeNull();
    await act(async () =>
    {
      button.dispatchEvent(new MouseEvent("click", { bubbles: true }));
    });

    await act(async () =>
    {
      let name = document.getElementById('new-problem-bet-name') as HTMLInputElement;
      fireEvent.change(name, { target: { value: 'Name' } });
    });

    await act(async () =>
    {
      let description = document.getElementById('new-problem-bet-description') as HTMLInputElement;
      fireEvent.change(description, { target: { value: 'Name' } });
    });

    await act(async () =>
    {
      let success = document.getElementById('new-problem-bet-success') as HTMLInputElement;
      fireEvent.change(success, { target: { value: 'Success' } });
    });

    await act(async () =>
    {
      let buttonSubmit = document.getElementById('problem-bet-new-create') as HTMLButtonElement;
      buttonSubmit.dispatchEvent(new MouseEvent("click", { bubbles: true }));
    });
  });

  it('Displays the error if existing item with same name', async () =>
  {
    const problem: IProject = { name: "Name", problems: [], isLoaded: false, description: "Description" };
    jest.spyOn(ProjectService, "Get").mockReturnValue(Promise.resolve(problem));

    var init = { "status" : 400  };
    var myResponse = new Response(JSON.stringify({message: "There is already a bet with the same name"}),init);
    jest.spyOn(ProblemService, "Put").mockReturnValue(Promise.resolve(myResponse));

    // Render the projects page
    let pageData = {
      setBreadCrumbs: () => { return true; },
      breadCrumbs: Array<BreadCrumbItem>(),
      globalMessage: () => { return; }
    }
    act(() =>
    {
      ReactDOM.render(
        <MemoryRouter>
          <Project {...pageData} />
        </MemoryRouter>, container);
    });

    // The dialog window should not be visible.
    expect(document.getElementById('newModal')).not.toHaveClass("show");

    // Open the new dialog.
    let button = document.getElementById('add-new') as HTMLButtonElement;
    expect(button).not.toBeNull();
    await act(async () =>
    {
      button.dispatchEvent(new MouseEvent("click", { bubbles: true }));
    });

    await act(async () =>
    {
      let name = document.getElementById('new-problem-bet-name') as HTMLInputElement;
      fireEvent.change(name, { target: { value: 'Name' } });
    });

    await act(async () =>
    {
      let description = document.getElementById('new-problem-bet-description') as HTMLInputElement;
      fireEvent.change(description, { target: { value: 'Name' } });
    });

    await act(async () =>
    {
      let success = document.getElementById('new-problem-bet-success') as HTMLInputElement;
      fireEvent.change(success, { target: { value: 'Success' } });
    });

    await act(async () =>
    {
      let buttonSubmit = document.getElementById('problem-bet-new-create') as HTMLButtonElement;
      buttonSubmit.dispatchEvent(new MouseEvent("click", { bubbles: true }));
    });
    let errorMessage = document.getElementById("validationNameFeedback")
    expect(errorMessage?.innerHTML).toEqual("There is already a bet with the same name");

  });
});

