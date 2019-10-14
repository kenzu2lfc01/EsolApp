export class TodoModel {
    id?: number;
    todoName: string;
    status: boolean;
    description: string;
    modifyDate: Date;
}

export class TodoViewModel {
    TodoName: string;
    Description: string;
}
