export default function TabComments()
{
    return <>
        <div className="mb-3 mt-2">
            <label htmlFor="comment">Add a new comment</label>
            <textarea
                id="comment"
                name="comment"
                placeholder="New comment..."
                className="form-control mt-2"
            ></textarea>
            <button className="btn btn-secondary mt-3 mb-3">Add Comment</button>
            <p>No Existing comments</p>
        </div>
    </>
}