import { ImageModel } from '../images/image.model';

export class TodoModel {
    id?: number;
    todoName: string;
    status: boolean;
    description: string;
    modifyDate: Date;
    imageViewModels: Array<ImageModel> = [];
}

export class TodoViewModel {
    TodoName: string;
    Description: string;
}

export class TodoPatchViewModel {
    id: number;
    TodoName: string;
    Description: string;
}
export class LoginShareViewModel {
    UserId: Array<string>;
    TodoId: number;
}
