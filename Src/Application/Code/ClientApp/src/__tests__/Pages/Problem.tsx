import ReactDOM from 'react-dom';
import { act } from 'react-dom/test-utils';
import { MemoryRouter } from 'react-router';
import { BreadCrumbItem } from '../../Components/BreadCrumbs';
import { IProblem } from '../../Interfaces/IPage';
import { Problem } from '../../Pages/Problem';
import { ProblemService } from '../../Services/ProblemService';

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
      globalMessage: () => { return; }
    }
    await act(async () =>
    {
      ReactDOM.render(
        <MemoryRouter>
          <Problem {...pageData} />
        </MemoryRouter>, container);
    });
  });

  it('can display the add new bet form', async () =>
  {
    const problem: IProblem = { name: "Name", bets: [], isLoaded: false, description: "Description", successCriteria: "" };
    jest.spyOn(ProblemService, "Get").mockReturnValue(Promise.resolve(problem));

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

    let name = document.getElementById('new-problem-bet-name') as HTMLInputElement;
    name.value = "test";

    let description = document.getElementById('new-problem-bet-description') as HTMLInputElement;
    description.value = "test";

    let success = document.getElementById('new-problem-bet-success') as HTMLInputElement;
    success.value = "test";

    let buttonSubmit = document.getElementById('add-new') as HTMLButtonElement;
    buttonSubmit.dispatchEvent(new MouseEvent("click", { bubbles: true }));
  });
});

