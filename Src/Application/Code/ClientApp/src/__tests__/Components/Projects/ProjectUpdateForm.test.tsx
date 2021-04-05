import ReactDOM from 'react-dom';
import { act } from 'react-dom/test-utils';
import ProjectUpdateForm from '../../../Components/Projects/ProjectUpdateForm';
import { IPage, IProblem, IProject } from '../../../Interfaces/IPage';

let container: HTMLDivElement | null;

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

describe(`The project / bet update form component`, () =>
{
  it('can render', () =>
  {
    act(() =>
    {
      let project: IProject = { name: "Project Name", description: "Project description" } as IProject;
      let pageProps: IPage = {} as IPage;
      ReactDOM.render(<ProjectUpdateForm projectId="ProjectId" project={project} pageProps={pageProps} />, container);
    });
  })

  it('can validate the form', async () =>
  {
    // display form.
    act(() =>
    {
      let project: IProject = { name: "", description: "Project description" } as IProject;
      let pageProps: IPage = {} as IPage;
      ReactDOM.render(<ProjectUpdateForm projectId="ProjectId" project={project} pageProps={pageProps} />, container);
    });

    // Try and submit form.
    let button = document.getElementById('project-update');
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

