import ReactDOM from 'react-dom';
import { act } from 'react-dom/test-utils';
import ProblemUpdateForm from '../../../Components/Problem/ProblemUpdateForm';
import { IPage, IProblem } from '../../../Interfaces/IPage';

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

describe(`The problem / bet new form component`, () => {
  it('can render', () => {
    act(() =>
    {
      let problem: IProblem = { name: "Problem Name", description: "Problem description" } as IProblem;
      let pageProps: IPage = {} as IPage;
      ReactDOM.render(<ProblemUpdateForm projectId="ProjectId" problemId="ProblemId" problem={problem} pageProps={pageProps}/>, container);
    });
  })

  it('can validate the form', async () => {
    // display form.
    act(() => {
      let problem: IProblem = { name: "", description: "" } as IProblem;
      let pageProps: IPage = {} as IPage;
      ReactDOM.render(<ProblemUpdateForm projectId="ProjectId" problemId="ProblemId" problem={problem} pageProps={pageProps}/>, container);
    });

    // Try and submit form.
    let button = document.getElementById('problem-update');
    expect(button).not.toBeNull();

    if (button !== null)
    {
      expect(button.innerHTML).toBe("Update");
      await act(async () =>
      {
        if (button !== null)
        {
          button.dispatchEvent(new MouseEvent("click", { bubbles: true })); 
        }
      });
  
      // Check that there is an error for the name.
      let nameError = document.getElementById('validationNameFeedback');
      expect(nameError).not.toBeNull();

      if (nameError !== null)
      {
        expect(nameError.innerHTML).toBe("Required"); 
      }
    }
  });
});

