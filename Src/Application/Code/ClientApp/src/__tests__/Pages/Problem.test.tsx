import ReactDOM from 'react-dom';
import { fireEvent } from '@testing-library/react'
import { act } from 'react-dom/test-utils';
import { MemoryRouter } from 'react-router';
import { BreadCrumbItem } from '../../Components/BreadCrumbs';
import { IProblem } from '../../Interfaces/IPage';
import { Problem } from '../../Pages/Problem';
import { ProblemService } from '../../Services/ProblemService';
import { BetService } from '../../Services/BetService';

let container: HTMLElement | null;

beforeEach(() =>
{
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

describe(`The problem component`, () =>
{
  it('can render', async () =>
  {
    const problem: IProblem = { name: "Name", bets: [], isLoaded: false, description: "Description", successCriteria: "" };
    jest.spyOn(ProblemService, "Get").mockReturnValue(Promise.resolve(problem));

    let pageData = {
      setBreadCrumbs: () => { return true; },
      breadCrumbs: Array<BreadCrumbItem>(),
      globalMessage: () => { return; },
      globalMessageHide: () => { return; }
    }
    await act(async () =>
    {
      ReactDOM.render(
        <MemoryRouter>
          <Problem {...pageData} />
        </MemoryRouter>, container);
    });
  });

  it('can create a new bet successfully', async () =>
  {
    const problem: IProblem = { name: "Name", bets: [], isLoaded: false, description: "Description", successCriteria: "" };
    jest.spyOn(ProblemService, "Get").mockReturnValue(Promise.resolve(problem));

    var myBlob = new Blob();
    var init = { "status" : 202  };
    var myResponse = new Response(myBlob,init);
    jest.spyOn(BetService, "Put").mockReturnValue(Promise.resolve(myResponse));

    // Render the projects page
    let pageData = {
      setBreadCrumbs: () => { return true; },
      breadCrumbs: Array<BreadCrumbItem>(),
      globalMessage: () => { return; },
      globalMessageHide: () => { return; }
    }
    act(() =>
    {
      ReactDOM.render(
        <MemoryRouter>
          <Problem {...pageData} />
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

  it('displays error from server if one sent back on creation of bet', async () =>
  {
    const problem: IProblem = { name: "Name", bets: [], isLoaded: false, description: "Description", successCriteria: "" };
    jest.spyOn(ProblemService, "Get").mockReturnValue(Promise.resolve(problem));

    var init = { "status" : 400  };
    var myResponse = new Response(JSON.stringify({message: "There is already a bet with the same name"}),init);
    jest.spyOn(BetService, "Put").mockReturnValue(Promise.resolve(myResponse));

    // Render the projects page
    let pageData = {
      setBreadCrumbs: () => { return true; },
      breadCrumbs: Array<BreadCrumbItem>(),
      globalMessage: () => { return; },
      globalMessageHide: () => { return; }
    }
    act(() =>
    {
      ReactDOM.render(
        <MemoryRouter>
          <Problem {...pageData} />
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

