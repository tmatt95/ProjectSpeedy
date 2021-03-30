import { CardItem } from '../../Components/CardGrid';
import { IProject } from '../../Interfaces/IPage';
import { ProjectService } from '../../Services/ProjectService';

describe(`The project new form component`,() => {
  it('can get a project', async () =>
  {
    jest.spyOn(global, "fetch").mockImplementation((input: RequestInfo, init?: RequestInit | undefined) =>
    {
      let project: IProject = { description: "description", name: "ProjectName", problems: [], isLoaded: true };
      return Promise.resolve(new Response(JSON.stringify(project)));
    });
    expect((await ProjectService.Get("ProjectId")).name).toBe("ProjectName");
  });
});

