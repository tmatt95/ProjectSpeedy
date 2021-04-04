import { Formik } from 'formik';
import { IPage, IProject } from '../../Interfaces/IPage';
import { ProjectService } from '../../Services/ProjectService';

function getFormInputClass(showError: boolean, otherClasses: string): string
{
  if (showError)
  {
    return "is-invalid " + otherClasses
  }
  return otherClasses;
}

export default function ProjectUpdateForm({ projectId, project, pageProps }: { projectId: string, project: IProject, pageProps: IPage })
{
  if (project.isLoaded === false)
  {
    return <></>;
  }

  return (<>
    <Formik
      initialValues={{ name: project.name, description: project.description }}
      validate={values =>
      {
        const errors: { name: string, description: string } = {
          name: "",
          description: ""
        };
        let hasError: boolean = false;
        if (!values.name)
        {
          errors.name = 'Required';
          hasError = true;
        }
        if (hasError === true)
        {
          return errors;
        }
        else
        {
          return {};
        }
      }}
      onSubmit={async (values, { setSubmitting, setErrors, setStatus, resetForm }) =>
      {
        let saveResponse = await ProjectService.Post(projectId, JSON.stringify(values));
        if (saveResponse.status !== 202)
        {
          // Display error
          const json: { message: string } = await saveResponse.json();
          setErrors({ name: json.message });
        }
        else
        {
          pageProps.globalMessage({ message: "You have updated the project", class: "alert-success" })
        }
      }}
    >
      {({
        values,
        errors,
        touched,
        handleChange,
        handleBlur,
        handleSubmit,
        handleReset,
        isSubmitting,
        /* and other goodies */
      }) => (
        <form onReset={handleReset} onSubmit={handleSubmit}>
          <div className="mb-3">
            <label htmlFor="project-name" className="form-label">Name</label>
            <input
              id="project-name"
              type="text"
              name="name"
              onChange={handleChange}
              onBlur={handleBlur}
              value={values.name}
              className={getFormInputClass(errors !== undefined && errors.name !== undefined && errors.name.length > 0, "form-control")}
            />
          </div>

          <div className="mb-3">
            <label htmlFor="project-description" className="form-label">Description</label>
            <textarea
              id="project-description"
              name="description"
              onChange={handleChange}
              onBlur={handleBlur}
              value={values.description}
              className={getFormInputClass(errors !== undefined && errors.description !== undefined && errors.description.length > 0, "form-control")}
            ></textarea>
            <div id="validationDescriptionFeedback" className="invalid-feedback">
              {errors.description && touched.description && errors.description}
            </div>
          </div>

          <div className="row mb-3">
            <div className="col">
              <button type="submit" disabled={isSubmitting} className="btn btn-primary" id="bet-update">Update</button>
            </div>
          </div>

        </form>
      )}
    </Formik>
  </>);
}