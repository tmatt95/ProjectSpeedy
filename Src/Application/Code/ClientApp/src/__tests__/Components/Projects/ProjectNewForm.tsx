import { fireEvent } from '@testing-library/react';
import ReactDOM from 'react-dom';
import { act } from 'react-dom/test-utils';
import ProjectNewForm from '../../../Components/Projects/ProjectNewForm';
import { IProject } from '../../../Interfaces/IPage';
import { ProblemService } from '../../../Services/ProblemService';
import { ProjectService } from '../../../Services/ProjectService';

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

describe(`The project new form component`, () => {
  it('can render', () => {
    act(() => {
      ReactDOM.render(<ProjectNewForm setProjects={() => { return true }} />, container);
    });
  })

  it('can validate the form', async () => {

    // display form.
    act(() => {
      ReactDOM.render(<ProjectNewForm setProjects={() => { return true }} />, container);
    });

    // Try and submit form.
    let button = document.getElementById('project-new-create') as HTMLButtonElement;
    expect(button.innerHTML).toBe("Add Project");
    await act(async () => {
      button.dispatchEvent(new MouseEvent("click", { bubbles: true }));
    });

    // Check that there is an error for the name.
    let nameError = document.getElementById('validationNameFeedback') as HTMLButtonElement;
    expect(nameError.innerHTML).toBe("Required");
  });

  it('can submit a valid form', async () => {
    const problem: IProject = { name: "Name", problems: [], isLoaded: false, description: "Description"};
    jest.spyOn(ProjectService, "GetAll").mockReturnValue(Promise.resolve([]));

    var init = { "status" : 202  };
    var myResponse = new Response(JSON.stringify({}),init);
    jest.spyOn(ProjectService, "Put").mockReturnValue(Promise.resolve(myResponse));

    // display form.
    act(() => {
      ReactDOM.render(<ProjectNewForm setProjects={() => { return true }} />, container);
    });

    // Sets the name of the new project
    await act(async () =>
    {
      let name = document.getElementById('new-project-name') as HTMLInputElement;
      fireEvent.change(name, { target: { value: 'Name' } });
    });

    // Try and submit form.
    let button = document.getElementById('project-new-create') as HTMLButtonElement;
    expect(button.innerHTML).toBe("Add Project");
    await act(async () => {
      button.dispatchEvent(new MouseEvent("click", { bubbles: true }));
    });

    // Check that there is an error for the name.
    let nameError = document.getElementById('validationNameFeedback') as HTMLElement;
    expect(nameError.innerHTML).toBe("");
  });
});

