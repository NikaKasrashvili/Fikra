export interface Post{
    postID?: number,
    postTitle: string,
    postShortDescription: string,
    postFullDescription: string,
    postAnonymousAuthor: string,
    postIsPublished?: boolean,
    postPublishedByID?: number,
    postPublisherFirstname: string,
    postPublisherLastname: string,
    postPublisherEmail: string,
    postUploadDate: Date,
    postPublishDate: Date,
    postSortIndex: number,
    postImageBase64: string,
    postFileLocation: string,
    postIsDeleted: boolean
}