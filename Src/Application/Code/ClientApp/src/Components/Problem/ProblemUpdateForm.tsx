import { Formik } from 'formik';
import { IPage, IProblem } from '../../Interfaces/IPage';
import { ProblemService } from '../../Services/ProblemService'; 

function getFormInputClass(showError: boolean, otherClasses: string): string
{
  if (showError)
  {
    return "is-invalid " + otherClasses
  }
  return otherClasses;
}

export default function ProblemUpdateForm({ projectId, problemId, problem, pageProps }: { projectId: string, problemId: string, problem: IProblem, pageProps: IPage })
{
  if (problem.isLoaded === false)
  {
    return <></>;
  }

  return (<>
    <Formik
      initialValues={{ name: problem.name, description: problem.description, successCriteria: problem.successCriteria }}
      validate={values =>
      {
        const errors: { name: string, description: string, successCriteria: string } = {
          name: "",
          description: "",
          successCriteria: ""
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
        let saveResponse = await ProblemService.Post(projectId, problemId, JSON.stringify(values));
        if (saveResponse.status !== 202)
        {
          // Display error
          const json: { message: string } = await saveResponse.json();
          setErrors({ name: json.message });
        }
        else
        {
          pageProps.globalMessage({ message: "You have updated the problem", class: "alert-success" })
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
           <h2>Info</h2>
          <div className="mb-3">
            <label htmlFor="problem-name" className="form-label">Name</label>
            <input
              id="problem-name"
              type="text"
              name="name"
              onChange={handleChange}
              onBlur={handleBlur}
              value={values.name}
              className={getFormInputClass(errors !== undefined && errors.name !== undefined && errors.name.length > 0, "form-control")}
            />
          </div>

          <div className="mb-3">
            <label htmlFor="problem-description" className="form-label">Description</label>
            <textarea
              id="problem-description"
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

          <div className="mb-3">
            <label htmlFor="problem-success" className="form-label">Success Criteria</label>
            <textarea
              id="problem-success"
              name="successCriteria"
              onChange={handleChange}
              onBlur={handleBlur}
              value={values.successCriteria}
              className={getFormInputClass(errors !== undefined && errors.successCriteria !== undefined && errors.successCriteria.length > 0, "form-control")}
            ></textarea>
            <div id="validationDescriptionFeedback" className="invalid-feedback">
              {errors.successCriteria && touched.successCriteria && errors.successCriteria}
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