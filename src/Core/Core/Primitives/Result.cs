﻿namespace Core.Primitives;

public class Result<T>
{
	public bool IsSuccess { get; set; }

	public T? Value { get; set; }

	public Error? Error { get; set; }

	public static Result<T> Success(T value) => new() { IsSuccess = true, Value = value };

	public static Result<T> Failure(Error error) => new() { IsSuccess = false, Error = error };
}